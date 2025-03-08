import React, { useState } from "react";
import { Drawer, List, ListItem, ListItemIcon, ListItemText, IconButton } from "@mui/material";
import MenuIcon from "@mui/icons-material/Menu";
import PeopleIcon from "@mui/icons-material/People";
import PaymentIcon from "@mui/icons-material/Payment";

const SidebarMenu = ({ onSelect }) => {
  const [open, setOpen] = useState(false);

  const toggleDrawer = (state) => () => {
    setOpen(state);
  };

  
  return (
    <>
      <IconButton onClick={toggleDrawer(true)} color="inherit">
        <MenuIcon />
      </IconButton>

      <Drawer anchor="left" open={open} onClose={toggleDrawer(false)}>
        <List>
          <ListItem button onClick={() => onSelect("manageClients")} style={{ cursor: 'pointer' }}>
            <ListItemIcon>
              <PeopleIcon />
            </ListItemIcon>
            <ListItemText primary="Gerenciar Clientes" />
          </ListItem>

          <ListItem button onClick={() => onSelect("createPayment")} style={{ cursor: 'pointer' }}>
            <ListItemIcon>
              <PaymentIcon />
            </ListItemIcon>
            <ListItemText primary="Criar Pagamentos" />
          </ListItem>

          <ListItem button onClick={() => onSelect("summary")} style={{ cursor: 'pointer' }}>
            <ListItemIcon>
              <PaymentIcon />
            </ListItemIcon>
            <ListItemText primary="Resumo" />
          </ListItem>
        </List>
      </Drawer>
    </>
  );
};

export default SidebarMenu;
