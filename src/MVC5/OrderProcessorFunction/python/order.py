from dataclasses import dataclass, asdict


@dataclass
class Order:
    LastName: str
    FirstName: str
    Address: str
    City: str
    State: str
    PostalCode: str
    Country: str
    Email: str
    Phone: str
    Username: str

    def __str__(self) -> str:
        return (
            f"OrderInfo - (LastName={self.LastName}, "
            f"FirstName={self.FirstName}, "
            f"Address={self.Address}, "
            f"City={self.City}, "
            f"State={self.State}, "
            f"PostalCode={self.PostalCode}, "
            f"Country={self.Country}, "
            f"Email={self.Email}, "
            f"Phone={self.Phone}, "
            f"Username={self.Username})"
        )
        
    def to_dict(self) -> dict:
        return asdict(self)