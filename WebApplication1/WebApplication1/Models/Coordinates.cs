namespace WebApplication1.Models
{
    public class Coordinates : IEquatable<Coordinates>
    {
        public int y {  get; set; }
        public int x { get; set; }
        public bool Equals(Coordinates other)
        {
            if (this.y == other.y && this.x == other.x)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}
