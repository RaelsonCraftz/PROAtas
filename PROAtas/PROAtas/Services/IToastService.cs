namespace PROAtas.Services
{
    // Exemplo encontrado em:
    // https://stackoverflow.com/questions/35279403/toast-equivalent-for-xamarin-forms
    public interface IToastService
    {
        void LongAlert(string message);
        void ShortAlert(string message);
    }
}
