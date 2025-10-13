﻿namespace Shop.Models.Kindergarten
{
    public class KindergartenCreateUpdateViewModel
    {
        public Guid? Id { get; set; }
        public string GroupName { get; set; }
        public int ChidlrenCount { get; set; }
        public string KindergartenName { get; set; }
        public string TeacherName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public List<IFormFile> Files { get; set; }
        public List<KindergartenImageViewModel> Image { get; set; }
            = new List<KindergartenImageViewModel>();
    }
}
