import React from "react";
import {
  Box,
  Button,
  Stack,
  TextField,
  Snackbar,
  Alert,
} from "@mui/material";

import { Profile } from "../models/profile";
import { createProfile } from "../api/client";

interface Props {
  onSaved: () => void;
}

export const ProfileForm: React.FC<Props> = ({ onSaved }) => {
  const [successOpen, setSuccessOpen] = React.useState(false);
  const [errorOpen, setErrorOpen] = React.useState(false);

  const [profile, setProfile] = React.useState<Profile>({
    id: 0,
    firstName: "",
    lastName: "",
    contactEMail: "",
    contactPhone: "",
    address1: "",
    address2: "",
  });

const handleSubmit = async () => {
    try {
      await createProfile(profile);

      setSuccessOpen(true);

      setProfile({
        id: 0,
        firstName: "",
        lastName: "",
        contactEMail: "",
        contactPhone: "",
        address1: "",
        address2: "",
      });

      onSaved();
    } catch (error) {
      setErrorOpen(true);
    }
  };
    return (
    <div>
      <h2 style={{ marginBottom: 20 }}>Dodaj profil</h2>

      <Box>
        <Stack spacing={2}>
          <TextField
            label="ID"
            type="number"
            value={profile.id}
            onChange={(e) =>
              setProfile({ ...profile, id: Number(e.target.value) })
            }
            fullWidth
          />

          <TextField
            label="Imię"
            value={profile.firstName}
            onChange={(e) =>
              setProfile({ ...profile, firstName: e.target.value })
            }
            fullWidth
          />

          <TextField
            label="Nazwisko"
            value={profile.lastName}
            onChange={(e) =>
              setProfile({ ...profile, lastName: e.target.value })
            }
            fullWidth
          />

          <TextField
            label="E-mail"
            value={profile.contactEMail}
            onChange={(e) =>
              setProfile({ ...profile, contactEMail: e.target.value })
            }
            fullWidth
          />

          <TextField
            label="Adres 1"
            value={profile.address1}
            onChange={(e) =>
              setProfile({ ...profile, address1: e.target.value })
            }
            fullWidth
          />

          <TextField
            label="Adres 2"
            value={profile.address2}
            onChange={(e) =>
              setProfile({ ...profile, address2: e.target.value })
            }
            fullWidth
          />

          <Button
            variant="contained"
            size="large"
            onClick={handleSubmit}
          >
            Zapisz profil
          </Button>
        </Stack>

        <Snackbar
          open={successOpen}
          autoHideDuration={3000}
          onClose={() => setSuccessOpen(false)}
        >
          <Alert severity="success">Profil zapisany</Alert>
        </Snackbar>

        <Snackbar
          open={errorOpen}
          autoHideDuration={3000}
          onClose={() => setErrorOpen(false)}
        >
          <Alert severity="error">Błąd zapisu profilu</Alert>
        </Snackbar>
      </Box>
    </div>
  );
};