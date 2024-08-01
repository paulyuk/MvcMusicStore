from dataclasses import dataclass

@dataclass
class Order: 
    def __init__(self, LastName, FirstName, Address, City, State, PostCode, Country, Email, Phone, Username):
        self.LastName = LastName
        self.FirstName = FirstName
        self.Address = Address
        self.City = City
        self.State = State
        self.PostCode = PostCode
        self.Country = Country
        self.Email = Email
        self.Phone = Phone
        self.Username = Username

    def __str__(self):
        return (f"Order(LastName={self.LastName}, FirstName={self.FirstName}, Address={self.Address}, "
                f"City={self.City}, State={self.State}, PostCode={self.PostCode}, Country={self.Country}, "
                f"Email={self.Email}, Phone={self.Phone}, Username={self.Username})")
    