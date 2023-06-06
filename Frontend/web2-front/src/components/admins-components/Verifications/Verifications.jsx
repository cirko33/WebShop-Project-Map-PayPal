import { useEffect, useState } from "react";
import adminService from "../../../services/adminService";
import classes from "./Verifications.module.css";
import WaitingTable from "./WaitingTable/WaitingTable";
import UserTable from "./UserTable/UserTable";

const Verifications = () => {
  const [waitingUsers, setWaitingUsers] = useState([]);
  const [verifiedUsers, setVerifiedUsers] = useState([]);
  const [declinedUsers, setDeclinedUsers] = useState([]);
  const [buyers, setBuyers] = useState([]);

  const refresh = () => {
    adminService.getWaitingUsers().then((res) => {
      setWaitingUsers(res);
    });

    adminService.getVerifiedUsers().then((res) => {
      setVerifiedUsers(res);
    });

    adminService.getDeclinedUsers().then((res) => {
      setDeclinedUsers(res);
    });

    adminService.getBuyers().then((res) => {
      setBuyers(res);
    });
  };
  useEffect(() => {
    refresh();
  }, []);

  return (
    <div>
      {waitingUsers && waitingUsers.length !== 0 && (
        <>
          <h2 className={classes.heading}>Verifications</h2>
          <WaitingTable users={waitingUsers} refresh={refresh} />
          <br />
        </>
      )}
      {verifiedUsers && verifiedUsers.length !== 0 && (
        <>
          <h2 className={classes.heading}>Verified users</h2>
          <UserTable users={verifiedUsers} />
        </>
      )}
      {declinedUsers && declinedUsers.length !== 0 && (
        <>
          <h2 className={classes.heading}>Declined users</h2>
          <UserTable users={declinedUsers} />
        </>
      )}
      {buyers && buyers.length !== 0 && (
        <>
          <h2 className={classes.heading}>Buyers</h2>
          <UserTable users={buyers} />
        </>
      )}
    </div>
  );
};

export default Verifications;
