import React from "react";
import { DataGrid, GridColDef } from "@mui/x-data-grid";
import { Profile } from "../models/profile";

interface Props {
  profiles: Profile[];
  onSelect: (id: number) => void;
}

export const ProfileGrid: React.FC<Props> = ({ profiles, onSelect }) => {
  const columns: GridColDef[] = [
    {
      field: "id",
      headerName: "ID",
      width: 90,
    },
    {
      field: "firstName",
      headerName: "Imię",
      flex: 1,
    },
    {
      field: "lastName",
      headerName: "Nazwisko",
      flex: 1,
    },
    {
      field: "contactEMail",
      headerName: "E-mail",
      flex: 1.5,
    },
    {
      field: "contactPhone",
      headerName: "Telefon",
      flex: 1,
    },
  ];

  return (
    <div style={{ height: 450, width: "100%" }}>
      <DataGrid
        rows={profiles}
        columns={columns}
        pageSizeOptions={[5, 10, 20]}
        initialState={{
          pagination: {
            paginationModel: {
              pageSize: 5,
            },
          },
        }}
        onRowClick={(params) => onSelect(Number(params.row.id))}
        sx={{
          borderRadius: 4,
          border: "none",
          backgroundColor: "white",
          '& .MuiDataGrid-columnHeaders': {
            backgroundColor: '#f3f4f6',
            fontWeight: 'bold',
          },
        }}
      />
    </div>
  );
};
