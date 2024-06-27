namespace RecipeAppGUI
{
    internal class PieSlice
    {
        private string foodGroup;
        private int count;

        public PieSlice(string foodGroup, int count)
        {
            this.foodGroup = foodGroup;
            this.count = count;
        }

        public bool IsExploded { get; set; }
    }
}