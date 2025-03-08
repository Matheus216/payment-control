import { useState, useEffect } from "react";
import {
  TextField,
  Button,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Paper,
  Alert,
  Snackbar,
  TablePagination,
} from "@mui/material";

import api from '../services/api';

export default function ClientManager() {
  const [clients, setClients] = useState([]);
  const [name, setName] = useState("");
  const [email, setEmail] = useState("");
  const [error, setError] = useState("");
  const [success, setSuccess] = useState(false);
  const [page, setPage] = useState(1);
  const [pageSize, setPageSize] = useState(5);
  const [totalItems, setTotalItems] = useState(0);

  useEffect(() => {
    fetchClients();
  }, []);

  async function fetchClients(pageInput = 1, pageSizeInput = 5) {
    try {
      const response = await api.get('client', { 
        params: {
            'Pagination.Page': pageInput,
            'Pagination.PageSize': pageSizeInput
        }
      });
      if (!response.data.success) response.data.errorMessages.each(x => setError(x));
      setClients(response.data.data); 
      setTotalItems(response.data.totalItems);
    } catch (err) {
      setError(err.message);
    }
  }

  async function handleSubmit(e) {
    e.preventDefault();
    setError("");
    setSuccess(false);

    if (!name.trim() || !email.trim()) {
      setError("Nome e e-mail são obrigatórios.");
      return;
    }

    try {
      const response = await fetch("http://localhost:5198/api/client", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ name, email }),
      });

      if (!response.ok) throw new Error("Erro ao cadastrar cliente");

      setSuccess(true);
      setName("");
      setEmail("");
      await fetchClients(); 
    } catch (err) {
      setError(err.message);
    }
  }

  async function handleChangePage(_, newPage){
    console.log(newPage)
    setPage(newPage);
    await fetchClients(newPage, pageSize)
  } 
  async function handleChangeRowsPerPage(event) {
    setPageSize(parseInt(event.target.value, 10));
    setPage(1);
    await fetchClients(page, parseInt(event.target.value, 10))
  };

  return (
    <div style={{ maxWidth: "600px", margin: "auto", padding: "20px" }}>
      <h1>Gerenciador de Clientes</h1>

      <form onSubmit={handleSubmit} style={{ marginBottom: "20px" }}>
        <TextField
          label="Nome"
          fullWidth
          value={name}
          onChange={(e) => setName(e.target.value)}
          margin="normal"
        />
        <TextField
          label="E-mail"
          type="email"
          fullWidth
          value={email}
          onChange={(e) => setEmail(e.target.value)}
          margin="normal"
        />
        {error && <Alert severity="error">{error}</Alert>}
        <Button
          type="submit"
          variant="contained"
          color="primary"
          fullWidth
          style={{ marginTop: "10px" }}
        >
          Cadastrar Cliente
        </Button>
      </form>

      <Snackbar
        open={success}
        autoHideDuration={3000}
        onClose={() => setSuccess(false)}
        anchorOrigin={{ vertical: "top", horizontal: "center" }}
      >
        <Alert onClose={() => setSuccess(false)} severity="success">
          Cliente cadastrado com sucesso!
        </Alert>
      </Snackbar>

      {/* Tabela de Clientes */}
      <TableContainer component={Paper} style={{ maxHeight: 300 }}>
        <Table stickyHeader>
          <TableHead>
            <TableRow>
              <TableCell>ID</TableCell>
              <TableCell>Nome</TableCell>
              <TableCell>E-mail</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {clients.length > 0 ? (
              clients
                .map((client) => (
                  <TableRow key={client.id}>
                    <TableCell>{client.id}</TableCell>
                    <TableCell>{client.name}</TableCell>
                    <TableCell>{client.email}</TableCell>
                  </TableRow>
                ))
            ) : (
              <TableRow>
                <TableCell colSpan={3} align="center">
                  Nenhum cliente cadastrado.
                </TableCell>
              </TableRow>
            )}
          </TableBody>
        </Table>
      </TableContainer>

      {/* Paginação */}
      <TablePagination
        rowsPerPageOptions={[5, 10, 20]}
        component="div"
        count={totalItems}
        rowsPerPage={pageSize}
        page={page}
        onPageChange={handleChangePage}
        onRowsPerPageChange={handleChangeRowsPerPage}
        
      />
    </div>
  );
}
