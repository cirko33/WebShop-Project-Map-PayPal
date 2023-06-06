import { Button, Card, CardActions, CardContent, Typography } from "@mui/material";
import Item from "./Item";
import { dateTimeToString } from "../../helpers/helpers";
import { useContext, useEffect, useState } from "react";
import AuthContext from "../../contexts/auth-context";
import buyerService from "../../services/buyerService";

const Orders = ({ orders, title, updateOrders }) => {
  const status = (o) => {
    return o.isCancelled ? "Cancelled" : new Date(o.deliveryTime) > new Date() ? "In delivery" : "Delivered";
  };
  const context = useContext(AuthContext);
  const [countdowns, setCountdowns] = useState({});
  const delHours = 1000 * 60 * 60;
  const delMinutes = 1000 * 60;

  useEffect(() => {
    console.log(context.type());
    setInterval(() => {
      const temp = {};
      for (const key in countdowns) {
        temp[key] = countdowns[key] - 1;
      }
      setCountdowns(temp);
    }, 1000);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const timeToDeliver = (remaining) => {
    const hours = Math.floor(remaining / delHours);
    const minutes = Math.floor((remaining % delHours) / delMinutes);
    const seconds = Math.floor((remaining % delMinutes) / 1000);
    return `${hours.toString().padStart(2, "0")}:${minutes.toString().padStart(2, "0")}:${seconds
      .toString()
      .padStart(2, "0")}`;
  };

  const canBeCancelled = (orderTime) => {
    if (new Date() - new Date(orderTime) > delHours) return false;
    return true;
  };

  return (
    <div>
      <Typography variant="h4">{title}</Typography>
      {orders &&
        orders.length > 0 &&
        orders.map((o, index) => (
          <Card key={index} sx={{ minWidth: 300, background: "gray", color: "white", marginTop:"10px" }}>
            {!countdowns[index] &&
              status(o) === "In delivery" &&
              setCountdowns({ ...countdowns, [index]: new Date(o.deliveryTime) - new Date() })}
            <CardContent>
              <Typography>Ordered: {dateTimeToString(o.orderTime)}</Typography>
              {status(o) === "In delivery" && !context.inType("Administrator") && (
                <Typography>Time to deliver: {timeToDeliver(countdowns[index])}</Typography>
              )}
              <Typography>Address: {o.deliveryAddress}</Typography>
              <Typography>Status: {status(o)}</Typography>
              <Typography sx={{ fontWeight: "bold", color: "lightblue" }}>Items:</Typography>
              {o.items.map((item, index) => (
                <Item key={index} item={item} />
              ))}
              <hr />
              <Typography>Comment: {o.comment}</Typography>
              <Typography>Total: {o.orderPrice}</Typography>
            </CardContent>
            {context.type() === "Buyer" && canBeCancelled(o.orderTime) && (
              <CardActions>
                <Button onClick={e => {buyerService.postCancel(o.id).then(res => updateOrders())}}>Cancel</Button>
              </CardActions>
            )}
          </Card>
        ))}
      {orders.length === 0 && (
        <Typography variant="h5" sx={{ color: "blue" }}>
          There are no orders
        </Typography>
      )}
    </div>
  );
};

export default Orders;
