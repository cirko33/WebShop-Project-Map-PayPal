import { TableContainer, Paper, Table, TableHead, TableRow, TableBody, TableCell } from "@mui/material";
import { tableColumns } from "../../../../helpers/helpers";

const UserTable = ({ users }) => {
  return (
    <TableContainer component={Paper}>
      <Table sx={{ minWidth: 700 }} aria-label="simple table">
        <TableHead>
          <TableRow>
            {Object.keys(users[0]).map((key, index) => (
              <TableCell key={index}>{key}</TableCell>
            ))}
          </TableRow>
        </TableHead>
        <TableBody>
          {users.map((user, index) => (
            <TableRow key={index}>
              {Object.keys(user).map((key, index) => (
                <TableCell key={index}>{tableColumns(key, user)}</TableCell>
              ))}
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
};

export default UserTable;
