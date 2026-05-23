import React from "react";
import { Card, CardContent, Typography } from "@mui/material";

interface Props {
  title: string;
  children: React.ReactNode;
}

export const LayoutCard: React.FC<Props> = ({ title, children }) => {
  return (
    <Card
      elevation={4}
      sx={{
        borderRadius: 4,
        padding: 2,
        height: "100%",
      }}
    >
      <CardContent>
        <Typography
          variant="h5"
          sx={{ marginBottom: 3, fontWeight: 700 }}
        >
          {title}
        </Typography>

        {children}
      </CardContent>
    </Card>
  );
};
