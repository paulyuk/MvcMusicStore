import azure.functions as func
import logging
import json
from utils import BackendWorker
from order import Order

app = func.FunctionApp(http_auth_level=func.AuthLevel.FUNCTION)


@app.route(route="cart/{shoppingCartId}/order", methods=["POST"])
def order(req: func.HttpRequest) -> func.HttpResponse:
    logging.info("Received a request to process an order.")

    try:
        req_body = req.get_json()
    except ValueError:
        return func.HttpResponse("No order found", status_code=400)

    shopping_cart_id = req.route_params.get("shoppingCartId")
    order = Order(**req_body["order"])

    logging.info(
        f"Received order with shoppingCartId: {shopping_cart_id} from name: {order.FirstName} {order.LastName}"
    )
    logging.info(str(order))

    backend_worker = BackendWorker(order, logging)
    backend_worker.do_work()

    return func.HttpResponse(json.dumps(order.to_dict()), mimetype="application/json")
