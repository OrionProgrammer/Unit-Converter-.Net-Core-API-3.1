namespace App.Model.Helpers
{
    public class BaseModel : IBaseModel
    {
        public int Id { get; set; }

        public int GetId()
        {
            return Id;
        }

    }
}
