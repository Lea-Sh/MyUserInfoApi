﻿namespace MyUserInfoAPI.Models
{
    public enum Status
    {
        Failed,
        NotFound,
        ValidationError,
        Ok
    }

    public class Result<T>
    {
        public T Entity { get; set; }
        public Status Status { get; set; }
    }

}
