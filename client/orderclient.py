from __future__ import annotations
from kiota_abstractions.api_client_builder import enable_backing_store_for_serialization_writer_factory, register_default_deserializer, register_default_serializer
from kiota_abstractions.base_request_builder import BaseRequestBuilder
from kiota_abstractions.get_path_parameters import get_path_parameters
from kiota_abstractions.request_adapter import RequestAdapter
from kiota_abstractions.serialization import ParseNodeFactoryRegistry, SerializationWriterFactoryRegistry
from kiota_serialization_json.json_parse_node_factory import JsonParseNodeFactory
from kiota_serialization_json.json_serialization_writer_factory import JsonSerializationWriterFactory
from kiota_serialization_text.text_parse_node_factory import TextParseNodeFactory
from kiota_serialization_text.text_serialization_writer_factory import TextSerializationWriterFactory
from typing import Any, Callable, Dict, List, Optional, TYPE_CHECKING, Union

if TYPE_CHECKING:
    from .cart.cart_request_builder import CartRequestBuilder

class Orderclient(BaseRequestBuilder):
    """
    The main entry point of the SDK, exposes the configuration and the fluent API.
    """
    def __init__(self,request_adapter: RequestAdapter) -> None:
        """
        Instantiates a new Orderclient and sets the default values.
        param request_adapter: The request adapter to use to execute the requests.
        Returns: None
        """
        if not request_adapter:
            raise TypeError("request_adapter cannot be null.")
        super().__init__(request_adapter, "{+baseurl}", None)
        register_default_serializer(JsonSerializationWriterFactory)
        register_default_serializer(TextSerializationWriterFactory)
        register_default_deserializer(JsonParseNodeFactory)
        register_default_deserializer(TextParseNodeFactory)
        if not self.request_adapter.base_url:
            self.request_adapter.base_url = "https://apim-s7rg4drgann6s.azure-api.net/api"
        self.path_parameters["base_url"] = self.request_adapter.base_url
    
    @property
    def cart(self) -> CartRequestBuilder:
        """
        The cart property
        """
        from .cart.cart_request_builder import CartRequestBuilder

        return CartRequestBuilder(self.request_adapter, self.path_parameters)
    

