using CurrieTechnologies.Razor.SweetAlert2;
using Empresa.Frontend.Pages.Cargos;
using Empresa.Frontend.Repositories;
using Empresa.Shared.Entities;
using Microsoft.AspNetCore.Components;

namespace Empresa.Frontend.Pages.Empleados
{
    public partial class EmpleadoEditar
    {
        private Empleado? empleado=new();
        private EmpleadosForm? empleadosForm;
        [Inject] private NavigationManager NavigationManager { get; set; }=null!;

        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Parameter] public int Id { get; set; }

        protected override async Task OnInitializedAsync()
        {

            await LoadAsync();
        }
        private async Task LoadAsync()
        {
            var responseHTTP = await Repository.GetAsync<Empleado>($"api/empleados/{Id}");
            if (responseHTTP.Error)
            {
                if (responseHTTP.Httpresponsem.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    NavigationManager.NavigateTo("empleados");
                }
                else
                {
                    var messageError = await responseHTTP.GetErrorMessageAsync();
                    await SweetAlertService.FireAsync("Error", messageError, SweetAlertIcon.Error);
                }

            }
            else
            {
                empleado = responseHTTP.Response;
            }
        }
        private async Task EditAsync()
        {
            
            var responseHTTP = await Repository.PutAsync("api/empleados", empleado);
            if (responseHTTP.Error)
            {
                var mensajeError = await responseHTTP.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", mensajeError, SweetAlertIcon.Error);
                return;
            }
            Return();
            var toast = SweetAlertService.Mixin(new SweetAlertOptions
            {
                Toast = true,
                Position = SweetAlertPosition.BottomEnd,
                ShowConfirmButton = true,
                Timer = 3000
            });
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Cambios guardados con éxito.");
        }
        private void Return()
        {
            empleadosForm!.FormPostedSuccessfully = true;
            NavigationManager.NavigateTo("empleados");
        }
    }
}
