from order import Order

class BackendWorker:
    def __init__(self, order, logger):
        self.order = order
        self.logger = logger

    def do_work(self):
        self.logger.info("Processing order...")
        # Implement the actual work here

def json_to_order(json_data):
    return Order(**json_data)
