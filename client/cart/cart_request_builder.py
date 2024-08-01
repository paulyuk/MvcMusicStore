from __future__ import annotations
from kiota_abstractions.base_request_builder import BaseRequestBuilder
from kiota_abstractions.get_path_parameters import get_path_parameters
from kiota_abstractions.request_adapter import RequestAdapter
from typing import Any, Callable, Dict, List, Optional, TYPE_CHECKING, Union

if TYPE_CHECKING:
    from .item.with_shopping_cart_item_request_builder import WithShoppingCartItemRequestBuilder

class CartRequestBuilder(BaseRequestBuilder):
    """
    Builds and executes requests for operations under /cart
    """
    def __init__(self,request_adapter: RequestAdapter, path_parameters: Union[str, Dict[str, Any]]) -> None:
        """
        Instantiates a new CartRequestBuilder and sets the default values.
        param path_parameters: The raw url or the url-template parameters for the request.
        param request_adapter: The request adapter to use to execute the requests.
        Returns: None
        """
        super().__init__(request_adapter, "{+baseurl}/cart", path_parameters)
    
    def by_shopping_cart_id(self,shopping_cart_id: str) -> WithShoppingCartItemRequestBuilder:
        """
        Gets an item from the musicstore.cart.item collection
        param shopping_cart_id: Unique identifier of the item
        Returns: WithShoppingCartItemRequestBuilder
        """
        if not shopping_cart_id:
            raise TypeError("shopping_cart_id cannot be null.")
        from .item.with_shopping_cart_item_request_builder import WithShoppingCartItemRequestBuilder

        url_tpl_params = get_path_parameters(self.path_parameters)
        url_tpl_params["shoppingCartId"] = shopping_cart_id
        return WithShoppingCartItemRequestBuilder(self.request_adapter, url_tpl_params)
    

