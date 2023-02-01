namespace GameArchitecture.Weapon
{
    public interface IReloadable
    {
        public int Magazine { get; set; }
        public float ReloadTime { get; set; }
        public float CurrentMagazine { get; set; }

        public void Reload();
    }
}
