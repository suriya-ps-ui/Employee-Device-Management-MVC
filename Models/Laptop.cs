namespace Models{
    public class Laptop{
        public required string lapHostName{get;set;}
        public required string empId{get;set;}
        public required string lapModel{get;set;}
        public required string processor{get;set;}
        public required string storage{get;set;}
        public required string ram{get;set;}
        public DateOnly assignedOn{get;set;}
        public required string status{get;set;}

    }
}