import { PayPalButtons, PayPalScriptProvider } from "@paypal/react-paypal-js";
import { useContext } from "react";
import { CartContext } from "../../../contexts/cart-context";
import buyerService from "../../../services/buyerService";
import { useNavigate } from "react-router-dom";
import { Button } from "@mui/material";

const Payment = () => {
  const { cart, setCart, data, setData } = useContext(CartContext);
  const navigate = useNavigate();

  const handlePayPal = async (data, actions) => {
    const temp = [];
    for (const i in cart) {
      temp.push({ productId: i, amount: cart[i] });
    }

    const price = await buyerService.getPrice(temp);

    return actions.order.create({
      purchase_units: [
        {
          amount: {
            value: price.toFixed(2).toString(),
            currency_code: "USD",
          },
        },
      ],
    });
  };

  const handleOrder = async () => {
    const sendData = { ...data, items: [] };
    for (const i in cart) {
      if (cart[i] > 0) sendData.items.push({ productId: i, amount: cart[i] });
    }
    console.log("🚀 ~ file: Payment.jsx:39 ~ handleOrder ~ sendData:", sendData);
    await buyerService.postOrder(sendData);
    setCart({});
    setData({});
    navigate("/previous-orders");
  };

  const handleApprove = (data, actions) => {
    return actions.order.capture().then(async (details) => {
      handleOrder();
    });
  };

  return (
    <>
      <div style={{ display: "flex", justifyContent: "center" }}>
        <div>
          <Button
            variant="contained"
            color="primary"
            onClick={handleOrder}
            style={{ marginBottom: "15px", width: "100%" }}
          >
            Pay when arrives
          </Button>
          <PayPalScriptProvider options={{ "client-id": process.env.REACT_APP_PAYPAL_CLIENT_ID }}>
            <PayPalButtons createOrder={handlePayPal} onApprove={handleApprove} style={{ label: "pay" }} />
          </PayPalScriptProvider>
        </div>
      </div>
    </>
  );
};

export default Payment;
