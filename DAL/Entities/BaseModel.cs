using System;

namespace DAL.Entities
{
    public class BaseModel
    {
        public DateTime CreationTime { get; set; } = DateTime.Now;

        public DateTime ModifiedTime { get; set; }

        public bool IsActive { get; set; } = true;







    }
}
