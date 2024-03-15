using CurrieTechnologies.Razor.SweetAlert2;
using Empresa.Frontend.Repositories;
using Empresa.Shared.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using System.Net;

namespace Empresa.Frontend.Pages.Empleados
{
    public partial class EmpleadosForm
    {

        private EditContext editContext = null!;
        [EditorRequired, Parameter] public Empleado Empleado { get; set; } = null!;
        [EditorRequired, Parameter] public EventCallback OnValidSubmit { get; set; }
        [EditorRequired, Parameter] public EventCallback ReturnAction { get; set; }
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;

        private List<Cargo>? Cargos;


        protected override async  Task OnInitializedAsync()
        {
            Empleado = new();
            editContext = new(Empleado);
            Empleado.Fecha_Ingreso = DateTime.Now;
            Empleado.Foto = "mipresona";
            Empleado.Id_Cargo = 2;
            await CargoAsyc();
            
        }

        private async Task CargoAsyc()
        {
            var responseHttp = await Repository.GetAsync<List<Cargo>>($"/api/cargos");
            if (responseHttp.Error)
            {
                if (responseHttp.Httpresponsem.StatusCode == HttpStatusCode.NotFound)
                {
                    NavigationManager.NavigateTo("/");
                    return;
                }
                var messageError = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", messageError, SweetAlertIcon.Error);
                return;
            }
            Cargos = responseHttp.Response;
        }
        public bool FormPostedSuccessfully { get; set; } = false;

        private async Task OnBeforeInternalNavigation(LocationChangingContext context)
        {
            var formWasEdited = editContext.IsModified();
            if (!formWasEdited || FormPostedSuccessfully)
            {
                return;
            }
            var result = await SweetAlertService.FireAsync(new SweetAlertOptions
            {
                Title = "Confirmación",
                Text = "¿Deseas abandonar la página y perder los cambios?",
                Icon = SweetAlertIcon.Warning,
                ShowCancelButton = true
            });
            var confirm = !string.IsNullOrEmpty(result.Value);
            if (confirm)
            {
                return;
            }
            context.PreventNavigation();
        }
    }
}
