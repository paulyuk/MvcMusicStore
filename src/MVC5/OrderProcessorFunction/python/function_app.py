import azure.functions as func
import logging
import json
from utils import BackendWorker, json_to_order

app = func.FunctionApp(http_auth_level=func.AuthLevel.FUNCTION)

@app.route(route="cart/{shoppingCartId}/order", methods=["POST"], auth_level=func.AuthLevel.FUNCTION)
def order(req: func.HttpRequest) -> func.HttpResponse:
    logging.info('Received a request to process an order.')

    try:
        req_body = req.get_json()
    except ValueError:
        return func.HttpResponse(
            "Invalid JSON",
            status_code=400
        )

    shopping_cart_id = req.route_params.get('shoppingCartId')
    order = json_to_order(req_body)

    logging.info(f"Received order with shoppingCartId: {shopping_cart_id} from name: {order.FirstName} {order.LastName}")
    logging.info(str(order))

    backend_worker = BackendWorker(order, logging)
    backend_worker.do_work()

    return func.HttpResponse(
        json.dumps(order.__dict__),
        mimetype="application/json",
        status_code=200
    )
   
