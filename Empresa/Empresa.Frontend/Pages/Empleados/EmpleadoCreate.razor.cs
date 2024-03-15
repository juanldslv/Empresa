using CurrieTechnologies.Razor.SweetAlert2;
using Empresa.Frontend.Pages.Cargos;
using Empresa.Frontend.Repositories;
using Empresa.Shared.Entities;
using Microsoft.AspNetCore.Components;


namespace Empresa.Frontend.Pages.Empleados
{
    
    public partial class EmpleadoCreate
    {
        private EmpleadosForm? empleadosForm;
        [Inject] private IRepository Repository { get; set; }=null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;

        private Empleado empleado = new();

        private async Task CreateAsync()
        {
            var responseHttp = await Repository.PostIAsync("/api/empleados", empleado);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message);
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
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Registro creado con éxito.");
        }

        private void Return()
        {
            empleadosForm!.FormPostedSuccessfully = true;
            NavigationManager.NavigateTo("/empleados");
        }
    }
}
