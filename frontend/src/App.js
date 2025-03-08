import React, { useState } from 'react';

import SidebarMenu from './components/SidebarMenu';
import { AppBar, Container, Toolbar, Typography } from '@mui/material';
import ClientManager from './components/ClientManager';
import PaymentManager from './components/PaymentManager';

const App = () => {
  const [selectedOption, setSelectedOption] = useState("");
  const [selectedClientId, setSelectedClient] = useState(0);

  const renderContent = () => {
    switch (selectedOption) {
      case "manageClients":
        return <ClientManager />;
      case "createPayment":
        return <PaymentManager />;
      default:
        return <ClientManager />;
    }
  };

  return (
    <div>
      <AppBar position="static">
        <Toolbar>
          <SidebarMenu onSelect={(option) => setSelectedOption(option)} />
          <Typography variant="h6" sx={{ flexGrow: 1 }}>
            {selectedOption === "manageClients" ? "Gerenciar Clientes" : 
             selectedOption === "createPayment" ? "Criar Pagamentos" : "Menu"}
          </Typography>
        </Toolbar>
      </AppBar>

      <Container>
        {renderContent()}
      </Container>

    </div>
  );
};

export default App;