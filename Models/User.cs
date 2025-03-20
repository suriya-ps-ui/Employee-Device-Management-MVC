namespace Models{
    public class User{
        public int id{get;set;}
        public required string userName{get;set;}
        public required string password{get;set;}
        public required string role{get;set;}
        public string? empId{get;set;}
    }
}