using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SysML_Sync.Services;

namespace SysML_Sync.Pages
{
    public partial class Moddel : ComponentBase
    {
        public bool Loaded = false;

        [Inject] public IModdelService ModdelService { get; set; }
        [Inject] public IJSRuntime JSRuntime { get; set; }

        [Inject] public NavigationManager NavigationManager { get; set; }

        public List<Model> Moddels { get; set; }=new List<Model>();
        public static int count { get; set; }

        private Timer debounceTimer;


        // Filter Parameters Object

        protected override async Task OnInitializedAsync()
        {
            try
            {
                Moddels = await ModdelService.GetAllAsync();
                count = Moddels.Count;
            }
            finally
            {
                Loaded = true;
                StateHasChanged();
            }
        }

        Dictionary<int, bool> showStates = new Dictionary<int, bool>();

        void ShowDropDowns(int modelId)
        {
            if (showStates.ContainsKey(modelId))
                showStates[modelId] = !showStates[modelId];
            else
                showStates[modelId] = true;
        }


    }
}
