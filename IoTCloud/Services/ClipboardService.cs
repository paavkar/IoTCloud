using Microsoft.JSInterop;

namespace IoTCloud.Services
{
    public class ClipboardService(IJSRuntime jsRuntime)
    {
        public ValueTask<string> ReadTextAsync()
        {
            return jsRuntime.InvokeAsync<string>("navigator.clipboard.readText");
        }

        public ValueTask WriteTextAsync(string text)
        {
            return jsRuntime.InvokeVoidAsync("navigator.clipboard.writeText", text);
        }
    }
}
