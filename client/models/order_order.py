from __future__ import annotations
from dataclasses import dataclass, field
from kiota_abstractions.serialization import AdditionalDataHolder, Parsable, ParseNode, SerializationWriter
from typing import Any, Callable, Dict, List, Optional, TYPE_CHECKING, Union

@dataclass
class Order_order(AdditionalDataHolder, Parsable):
    # Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.
    additional_data: Dict[str, Any] = field(default_factory=dict)

    # The Address property
    address: Optional[str] = None
    # The City property
    city: Optional[str] = None
    # The Country property
    country: Optional[str] = None
    # The Email property
    email: Optional[str] = None
    # The FirstName property
    first_name: Optional[str] = None
    # The LastName property
    last_name: Optional[str] = None
    # The Phone property
    phone: Optional[str] = None
    # The PostalCode property
    postal_code: Optional[str] = None
    # The State property
    state: Optional[str] = None
    # The Username property
    username: Optional[str] = None
    
    @staticmethod
    def create_from_discriminator_value(parse_node: ParseNode) -> Order_order:
        """
        Creates a new instance of the appropriate class based on discriminator value
        param parse_node: The parse node to use to read the discriminator value and create the object
        Returns: Order_order
        """
        if not parse_node:
            raise TypeError("parse_node cannot be null.")
        return Order_order()
    
    def get_field_deserializers(self,) -> Dict[str, Callable[[ParseNode], None]]:
        """
        The deserialization information for the current model
        Returns: Dict[str, Callable[[ParseNode], None]]
        """
        fields: Dict[str, Callable[[Any], None]] = {
            "Address": lambda n : setattr(self, 'address', n.get_str_value()),
            "City": lambda n : setattr(self, 'city', n.get_str_value()),
            "Country": lambda n : setattr(self, 'country', n.get_str_value()),
            "Email": lambda n : setattr(self, 'email', n.get_str_value()),
            "FirstName": lambda n : setattr(self, 'first_name', n.get_str_value()),
            "LastName": lambda n : setattr(self, 'last_name', n.get_str_value()),
            "Phone": lambda n : setattr(self, 'phone', n.get_str_value()),
            "PostalCode": lambda n : setattr(self, 'postal_code', n.get_str_value()),
            "State": lambda n : setattr(self, 'state', n.get_str_value()),
            "Username": lambda n : setattr(self, 'username', n.get_str_value()),
        }
        return fields
    
    def serialize(self,writer: SerializationWriter) -> None:
        """
        Serializes information the current object
        param writer: Serialization writer to use to serialize this model
        Returns: None
        """
        if not writer:
            raise TypeError("writer cannot be null.")
        writer.write_str_value("Address", self.address)
        writer.write_str_value("City", self.city)
        writer.write_str_value("Country", self.country)
        writer.write_str_value("Email", self.email)
        writer.write_str_value("FirstName", self.first_name)
        writer.write_str_value("LastName", self.last_name)
        writer.write_str_value("Phone", self.phone)
        writer.write_str_value("PostalCode", self.postal_code)
        writer.write_str_value("State", self.state)
        writer.write_str_value("Username", self.username)
        writer.write_additional_data_value(self.additional_data)
    

