namespace Models{
    public class Employee{
        public required string empId{get;set;}
        public required string empName{get;set;}
        public required string department{get;set;}
        public List<Laptop>? laptops{get;set;}
        public List<Keyboard>? keyboards{get;set;}
        public List<Mouse>? mouses{get;set;}
    }
}