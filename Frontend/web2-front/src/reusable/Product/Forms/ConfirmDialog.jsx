import { Button, Dialog, DialogActions, DialogContent, DialogTitle, TextField } from "@mui/material";
import { useContext, useRef, useState } from "react";
import { CartContext } from "../../../contexts/cart-context";
import { useNavigate } from "react-router-dom";
import classes from "./Forms.module.css";
import { Autocomplete, useJsApiLoader } from "@react-google-maps/api";

const ConfirmDialog = ({ open, setOpen, products }) => {
  const [libraries] = useState(["places"]);
  const { isLoaded } = useJsApiLoader({
    googleMapsApiKey: process.env.REACT_APP_GOOGLE_KEY,
    libraries,
    language: "en",
  });
  const ref = useRef();
  const { cart, data, setData } = useContext(CartContext);
  const navigate = useNavigate();
  const handleChange = (e) => {
    setData({
      ...data,
      [e.target.id]: e.target.value,
    });
  };

  const writeItems = () => {
    let temp = [];
    let total = 0;
    let sellers = [];
    for (const i in cart) {
      if (cart[i] !== 0) {
        const prod = products.find((it) => it.id === parseInt(i));
        if (!prod) continue;
        temp = [...temp, { ...prod, quantity: cart[i] }];
        total += prod.price * cart[i];
        if (!sellers.find((it) => it === prod.sellerId)) sellers = [...sellers, prod.sellerId];
      }
    }

    if (!isLoaded)
      return (
        <Dialog open={open} onClose={(e) => setOpen(false)} style={{ zIndex: 1 }}>
          <div>Loading...</div>
        </Dialog>
      );

    return (
      <>
        {temp.map((o, index) => (
          <div key={index}>
            <div className={classes.wrap}>
              <div className={classes.wrapLeft}>Name: {o.name}</div>
              <div className={classes.wrapRight}>
                <div>No: {o.quantity}</div>
                <div>Price: {o.price}</div>
              </div>
            </div>
            <hr />
          </div>
        ))}
        <div style={{ color: "red", fontSize: 20 }}>Total: {total.toFixed(2)}$ </div>
        <div style={{ color: "red", fontSize: 20 }}>Delivery: {sellers.length * 3.5}$ </div>
      </>
    );
  };

  const handleGoToPayment = async (e) => {
    
    if (!data.deliveryAddress || !data.positionX || !data.positionY) {
      alert("Please enter address");
      return;
    }
    navigate("/payment");
  };

  const handlePlaceChange = () => {
    const address = ref.current.getPlace();
    console.log("🚀 ~ file: ConfirmDialog.jsx:74 ~ handlePlaceChange ~ address:", address);
    if (address && address.formatted_address) {
      setData({
        ...data,
        deliveryAddress: address.formatted_address,
        positionX: address.geometry.location.lat(),
        positionY: address.geometry.location.lng(),
      });
    }
  };

  return (
    <Dialog open={open} onClose={(e) => setOpen(false)} style={{ zIndex: 1 }}>
      <DialogTitle>Confirm your order</DialogTitle>
      <DialogContent>
        <div>{writeItems()}</div>
        <Autocomplete onLoad={(cm) => (ref.current = cm)} onPlaceChanged={handlePlaceChange}>
          <TextField
            autoFocus
            margin="dense"
            id="deliveryAddress"
            label="Address"
            type="text"
            fullWidth
            variant="standard"
            value={data.deliveryAddress}
            onChange={handleChange}
          />
        </Autocomplete>
        <TextField
          autoFocus
          margin="dense"
          id="comment"
          label="Comment (optional)"
          type="text"
          fullWidth
          variant="standard"
          value={data.comment}
          onChange={handleChange}
        />
      </DialogContent>
      <DialogActions>
        <Button color="error" onClick={(e) => setOpen(false)}>
          Cancel
        </Button>
        <Button color="success" onClick={handleGoToPayment}>
          Go to payment
        </Button>
      </DialogActions>
    </Dialog>
  );
};

export default ConfirmDialog;
