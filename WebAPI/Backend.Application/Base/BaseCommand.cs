using Backend.Core.Entities;

namespace Backend.Application.Base
{
    public abstract record BaseCommand
    {
        public AppUser AppUser { get; set; }
    }
}