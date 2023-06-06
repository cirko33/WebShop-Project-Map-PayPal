import { useEffect, useState } from "react";
import buyerService from "../../../services/buyerService";
import Orders from "../../../reusable/Order/Orders";

const PreviousOrders = () => {
    const [orders, setOrders] = useState([]);
    const updateOrders = () => {
      buyerService.getOrders().then((res) => setOrders(res));
    }
    useEffect(() => {
      updateOrders();
    }, []);
    return <Orders orders={orders} title={"My orders"} updateOrders={updateOrders} />;
}
 
export default PreviousOrders;