import { useEffect, useState } from "react";
import { MapContainer, Marker, Popup, TileLayer } from "react-leaflet";
import L from "leaflet";
import sellerService from "../../../services/sellerService";
import "leaflet/dist/leaflet.css";
import { Button, Typography } from "@mui/material";
import { dateTimeToString } from "../../../helpers/helpers";
import Item from "../../../reusable/Order/Item";

const Map = () => {
  const startPosition = [45.25472833688446, 19.83317432993583];
  const [orders, setOrders] = useState(null);

  const refresh = async () => {
    try {
      const res = await sellerService.getNewOrders();
      setOrders(res);
    } catch (error) {
      console.error(error);
    }
  };

  const icon = new L.Icon({
    iconUrl: "package-icon.png",
    iconSize: [50, 50],
  });

  useEffect(() => {
    refresh();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const status = (o) => {
    return o.isCancelled
      ? "Cancelled"
      : !o.approved
      ? "Waiting for approval"
      : new Date(o.deliveryTime) > new Date()
      ? "In delivery"
      : "Delivered";
  };

  return (
    <MapContainer center={startPosition} zoom={13} style={{ width: "95vw", height: "90vh" }} scrollWheelZoom={true}>
      <TileLayer url={process.env.REACT_APP_MAP_API} />
      {orders &&
        orders.length !== 0 &&
        orders.map((o, i) => (
          <div key={i}>
            <Marker position={[o.positionX, o.positionY]} icon={icon}>
              <Popup style={{ background: "black" }}>
                <Typography>Ordered: {dateTimeToString(o.orderTime)}</Typography>
                <Typography>Address: {o.deliveryAddress}</Typography>
                <Typography>Status: {status(o)}</Typography>
                <Typography sx={{ fontWeight: "bold", color: "lightblue" }}>Items:</Typography>
                {o.items.map((item, index) => (
                  <Item key={index} item={item} />
                ))}
                <hr />
                <Typography>Comment: {o.comment}</Typography>
                <Typography>Total: {o.orderPrice.toFixed(2)}$</Typography>
                {!o.approved && (
                  <>
                    <Button
                      variant="contained"
                      color="success"
                      onClick={(e) => {
                        sellerService.postApprove(o.id).then((res) => refresh());
                      }}
                    >
                      Approve
                    </Button>
                  </>
                )}
              </Popup>
            </Marker>
          </div>
        ))}
    </MapContainer>
  );
};

export default Map;
