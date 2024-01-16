using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SysML_Sync.Services;
namespace SysML_Sync.Pages
{
    public partial class Mapper
    {
        public bool Loaded = false;

        [Inject] public IMapperService MapperService { get; set; }
        

        public List<SysML_Sync.Mapper> Mappers { get; set; } = new List<SysML_Sync.Mapper>();
        public static int count { get; set; }

     


        // Filter Parameters Object

        protected override async Task OnInitializedAsync()
        {
            try
            {
                Mappers = await MapperService.GetAllAsync();
                count = Mappers.Count;
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
