import React, { useEffect, useState } from "react";
import { 
    Card, 
    CardContent, 
    Typography, 
    Grid, 
    CircularProgress 
} from "@mui/material";

import api from "../services/api";

const PaymentSummary = () => {
  const [summary, setSummary] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    api.get("/payment/summary")
      .then(response => {
        setSummary(response.data.data);
        setLoading(false);
      })
      .catch(error => {
        setError("Failed to load payment summary");
        setLoading(false);
      });
  }, []);

  if (loading) return <CircularProgress />;
  if (error) return <Typography color="error">{error}</Typography>;

  return (
    <Grid container spacing={2}>
      <Grid item xs={12} sm={6} md={3}>
        <Card>
          <CardContent>
            <Typography variant="h6">Paid</Typography>
            <Typography variant="h4">{summary.totalPaid}</Typography>
          </CardContent>
        </Card>
      </Grid>
      <Grid item xs={12} sm={6} md={3}>
        <Card>
          <CardContent>
            <Typography variant="h6">Pending</Typography>
            <Typography variant="h4">{summary.totalPending}</Typography>
          </CardContent>
        </Card>
      </Grid>
      <Grid item xs={12} sm={6} md={3}>
        <Card>
          <CardContent>
            <Typography variant="h6">Cancelled</Typography>
            <Typography variant="h4">{summary.totalCanceled}</Typography>
          </CardContent>
        </Card>
      </Grid>
      <Grid item xs={12} sm={6} md={3}>
        <Card>
          <CardContent>
            <Typography variant="h6">Clients</Typography>
            <Typography variant="h4">{summary.totalClients}</Typography>
          </CardContent>
        </Card>
      </Grid>
    </Grid>
  );
};

export default PaymentSummary;
