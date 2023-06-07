import { Button, Dialog, DialogActions, DialogContent, DialogTitle, TextField } from "@mui/material";
import { useContext } from "react";
import { CartContext } from "../../../contexts/cart-context";
import { useNavigate } from "react-router-dom";
import classes from "./Forms.module.css";

const ConfirmDialog = ({ open, setOpen, products }) => {
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
    return (
      <>
        {temp.map((o) => (
          <>
            <div className={classes.wrap}>
              <div className={classes.wrapLeft}>Name: {o.name}</div>
              <div className={classes.wrapRight}>
                <div>No: {o.quantity}</div>
                <div>Price: {o.price}</div>
              </div>
            </div>
            <hr />
          </>
        ))}
        <div style={{ color: "red", fontSize: 20 }}>Total: {total}$ </div>
        <div style={{ color: "red", fontSize: 20 }}>Delivery: {sellers.length * 3.5}$ </div>
      </>
    );
  };

  return (
    <Dialog open={open} onClose={(e) => setOpen(false)}>
      <DialogTitle>Confirm your order</DialogTitle>
      <DialogContent>
        <div>{writeItems()}</div>
        <TextField
          autoFocus
          margin="dense"
          id="deliveryAddress"
          label="Address (leave blank if you want it to be your home address)"
          type="text"
          fullWidth
          variant="standard"
          value={data.deliveryAddress}
          onChange={handleChange}
        />
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
        <Button color="success" onClick={e => navigate("/payment")}>
          Go to payment
        </Button>
      </DialogActions>
    </Dialog>
  );
};

export default ConfirmDialog;
