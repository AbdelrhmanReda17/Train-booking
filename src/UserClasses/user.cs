namespace Train_booking.src.UserClasses;
    public abstract  class User
    {   public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public User(string FirstName, string LastName, string Gender, int Age, string Email , string Phone)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Gender = Gender;
            this.Age = Age;
            this.Email = Email;
            this.Phone = Phone;
        }
        public override string ToString()
        {
            return $"{FirstName} {LastName} | {Age} | {Gender} | {Email} | {Phone}";
        }
    }
