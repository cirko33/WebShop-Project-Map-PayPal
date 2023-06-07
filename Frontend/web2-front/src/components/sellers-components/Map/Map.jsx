import { useEffect, useState } from "react";
import { MapContainer, Marker, Popup, TileLayer } from "react-leaflet";
import L from "leaflet";
import Geocode from "react-geocode";
import sellerService from "../../../services/sellerService";
import "leaflet/dist/leaflet.css";

const Map = () => {
  const startPosition = [45.25472833688446, 19.83317432993583];
  const [orders, setOrders] = useState(null);

  Geocode.setApiKey(process.env.REACT_APP_GOOGLE_API);
  Geocode.setLanguage("en");
  Geocode.setRegion("rs");
  const refresh = async () => {
    try {
      const res = await sellerService.getMyOrders();
      const setter = await Promise.all(
        res.map(async (e) => {
          const response = await Geocode.fromAddress(e.deliveryAddress);
          const { lat, lng } = response.results[0].geometry.location;
          return { ...e, position: [lat, lng] };
        })
      );
      setOrders(setter);
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

  return (
    <MapContainer center={startPosition} zoom={13} style={{ width: "95vw", height: "90vh" }} scrollWheelZoom={true}>
      <TileLayer url={process.env.REACT_APP_MAP_API} />
      {
       orders && orders.length !== 0 && console.log(orders)
      }
      {orders && orders.length !== 0 && orders.map((o, i) => (
        <div key={i}>
          {console.log(o)}
          <Marker position={o.position} icon={icon}>
            <Popup>{o.deliveryAddress}</Popup>
          </Marker>
        </div>
      ))}
    </MapContainer>
  );
};

export default Map;
