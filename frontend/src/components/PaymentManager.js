import React, { useEffect, useState } from "react";
import {
  Card, CardContent, Typography, TextField, MenuItem, Button, Table, TableHead, TableBody, TableRow, TableCell,
  Dialog, DialogActions, DialogContent, DialogTitle, Snackbar, Alert
} from "@mui/material";

import api from "../services/api";

const PaymentManager = () => {
  const [selectedClient, setSelectedClient] = useState("")
  const [date, setDate] = useState("")
  const [value, setValue] = useState("")
  const [payments, setPayments] = useState([])
  const [status, setStatus] = useState(0)
  const [filterClient, setFilterClient] = useState("")
  const [clients, setClients] = useState([])
  const [message, setMessage] = useState({show: false, desc: '', type: 'success'})

  const [editDialogOpen, setEditDialogOpen] = useState(false)
  const [editPayment, setEditPayment] = useState(null)

  useEffect(() => {
    api.get('/client').then(response => {
      try {
        if (!response.data.success) response.data.errorMessages.forEach(x => raiseError(x))
        setClients(response.data.data)
      } catch (e) {
        if (e?.response?.data?.errorMessages[0])
          raiseError(e.response.data.errorMessages[0])
        else
          raiseError(e.message)
      }
    })
  },[])

  function raiseError(message) {
    setMessage({type:'error', desc: message, show: true})
  }

  function lanceSuccess(message) {
    setMessage({type: 'success', desc: message, show:true})
  }
  
  async function fetchPayment(clientId) {
    api.get(`/payment/${clientId}`).then(response => {
       try {
        if (!response.data.success) response.data.errorMessages.each(x => raiseError(x));
        setPayments(response.data.data); 
      } catch (e) {
         if (e?.response?.data?.errorMessages[0])
           raiseError(e.response.data.errorMessages[0])
         else
           raiseError(e.message)
       }
    })
  }
  async function updateStatusPayment() {
    try {
      const response = await api.put(`/payment/${editPayment.id}/status?status=${status}`)
       
      console.log('req' + response)
      if (!response.data.success) response.data.errorMessages.each(x => raiseError(x))
    }
    catch (e) {
      if (e?.response?.data?.errorMessages[0])
        raiseError(e.response.data.errorMessages[0])
      else
        raiseError(e.message)
    }
  }

  async function addPayment() {
    try {

      if (selectedClient == 0) {
        raiseError('Necessário informar o cliente')
        return
      }

      console.log(value)
      if (value == '0' || !value || value == '' || parseFloat(value) <= 0 ){
        raiseError('Necessário informar o valor')
        return
      }
      
      if (!date || date == ''){
        raiseError('Necessário informar a data')
        return
      }

      const response = await api.post('/payment', {
        clientId: selectedClient,
        value: parseFloat(value),
        date: date
      });
      if (!response.data.success) response.data.errorMessages.each(x => raiseError(x))

      lanceSuccess('Cadastrado pagamento com sucesso')
    }
    catch (e) {
       if (e?.response?.data?.errorMessages[0])
        raiseError(e.response.data.errorMessages[0])
      else
        raiseError(e.message)
    }
  }

  async function handleFilterClient(clientId) {
    try {
      setFilterClient(clientId)
      await fetchPayment(clientId)
      setFilterClient("")
    }catch(e) {
      console.log(e)
    }
  }
  async function handleAddPayment(){
    await addPayment()
    setSelectedClient("")
    setDate("")
    setValue("")
  };

  const handleEdit = (payment) => {
    setEditPayment(payment)
    setEditDialogOpen(true)
  };

  async function handleSaveEdit() {
    setEditDialogOpen(false)
    await updateStatusPayment()
    await fetchPayment(editPayment.clientId)
    setStatus(0)
  };

  const handleChangeStatus = (statuValue) => {
    setStatus(statuValue)
  }

  return (
    <Card sx={{ margin: 2, padding: 2 }}>
      <CardContent>
        <Typography variant="h5">Gerenciar Pagamentos</Typography>

        <TextField
          select fullWidth label="Selecionar Cliente" value={selectedClient}
          onChange={(e) => setSelectedClient(e.target.value)} sx={{ my: 1 }}
        >
          {clients.map((client) => (
            <MenuItem key={client.id} value={client.id}>
              {client.name}
            </MenuItem>
          ))}
        </TextField>

        <TextField fullWidth type="date" value={date} onChange={(e) => setDate(e.target.value)} sx={{ my: 1 }} />
        <TextField fullWidth label="Valor (R$)" type="number" value={value} onChange={(e) => setValue(e.target.value)} sx={{ my: 1 }} />

        <Button variant="contained" color="primary" onClick={handleAddPayment} sx={{ mt: 1 }}>
          Adicionar Pagamento
        </Button>

        <TextField
          placeholder="Para filtrar basta selecionar um cliente"
          select fullWidth label="Selectione um cliente para filtrar" value={filterClient}
          onChange={(e) => handleFilterClient(e.target.value)} sx={{ my: 2 }}
        >
          {clients.map((client) => (
            <MenuItem key={client.id} value={client.id}>
              {client.name}
            </MenuItem>
          ))}
        </TextField>

        <Table>
          <TableHead>
            <TableRow>
              <TableCell>Cliente</TableCell>
              <TableCell>Data</TableCell>
              <TableCell>Valor (R$)</TableCell>
              <TableCell>Status</TableCell>
              <TableCell>Ações</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {payments.length > 0 && payments.map((payment) => (
              <TableRow key={payment.id}>
                <TableCell>{clients.find((c) => c.id === payment.clientId)?.name}</TableCell>
                <TableCell>{payment.date}</TableCell>
                <TableCell>{payment.value.toFixed(2)}</TableCell>
                <TableCell>{payment.status == 1 ? 'Pendente' : payment.status == 2 ? 'Pago' : 'Cancelado'}</TableCell>
                <TableCell>
                  <Button variant="contained" color="secondary" onClick={() => handleEdit(payment)}>Editar</Button>
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>

        <Dialog open={editDialogOpen} onClose={() => setEditDialogOpen(false)}>
          <DialogTitle>Editar Pagamento</DialogTitle>
          <DialogContent>
            {editPayment && (
              <>
                <TextField
                  select fullWidth label="Filtrar status" value={status} style={{ width: 340}}
                  onChange={(e) => handleChangeStatus(e.target.value)} sx={{ my: 2 }}
                >
                  <MenuItem key={2} value={2}>
                    Pago
                  </MenuItem>
                  <MenuItem key={3} value={3}>
                    Cancelado
                  </MenuItem>
 
                </TextField>
              </>

            )}
          </DialogContent>
          <DialogActions>
            <Button onClick={() => setEditDialogOpen(false)}>Cancelar</Button>
            <Button variant="contained" color="primary" onClick={handleSaveEdit}>Salvar</Button>
          </DialogActions>
        </Dialog>
      </CardContent>
      <Snackbar
        open={message.show}
        autoHideDuration={3000}
        onClose={() => setMessage({ ...message, show: false})}
        anchorOrigin={{ vertical: "top", horizontal: "center" }}
      >
        <Alert onClose={() => setMessage({...message, show: false})} severity={message.type}>
          {message.desc}
        </Alert>
      </Snackbar>
    </Card>
    
  );
};

export default PaymentManager
