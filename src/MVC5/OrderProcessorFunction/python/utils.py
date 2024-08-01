from order import Order
import time

class BackendWorker:
    def __init__(self, order, logger):
        self.order = order
        self.logger = logger

    def do_work(self):
        self.logger.info("Processing order...")
        time.sleep(1.5)
        self.logger.info(f"Processed order {self.order}")
        # Implement the actual work here