namespace MyUserInfoAPI.Models
{
    public enum Status
    {
        Failed,
        Ok
    }

    public class Result<T>
    {
        public T Entity { get; set; }
        public Status Status { get; set; }
    }

}
