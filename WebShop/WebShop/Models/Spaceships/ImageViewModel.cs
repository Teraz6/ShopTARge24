﻿namespace WebShop.Models.Spaceships
{
    public class ImageViewModel
    {
        public Guid ImageId { get; set; }
        public string? Filepath { get; set; }
        public Guid? SpaceshipId { get; set; }
    }
}
