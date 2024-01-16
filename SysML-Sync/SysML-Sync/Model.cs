namespace SysML_Sync
{
    public class Model
    {
        public int Id { get; set; }

        public int IdModel { get; set; }
        public string Name { get; set; }
        public string UmlContent { get; set; }

        public string NotationContent {get; set;}
        public Modeltype Modeltype { get; set; }
        public string? Creator { get; set; }

    }
   

}
