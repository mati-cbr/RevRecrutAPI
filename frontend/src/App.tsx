import React from "react";

import { Profile } from "./models/profile";
import { getProfiles, getProfileById } from "./api/client";

import { Container, Grid, Typography } from "@mui/material";

import { ProfileGrid } from "./components/ProfileGrid";
import { ProfileForm } from "./components/ProfileForm";
import { ProfileDetails } from "./components/ProfileDetails";
import { LayoutCard } from "./components/LayoutCard";

function App() {
  const [profiles, setProfiles] = React.useState<Profile[]>([]);
  const [selectedProfile, setSelectedProfile] = React.useState<Profile | null>(null);

  const loadProfiles = async () => {
    try {
      const data = await getProfiles();
      setProfiles(data);
    } catch (error) {
      console.error(error);
    }
  };

  const handleSelect = async (id: number) => {
    try {
      const profile = await getProfileById(id);
      setSelectedProfile(profile);
    } catch (error) {
      console.error(error);
    }
  };

  React.useEffect(() => {
    loadProfiles();
  }, []);

   return (
    <Container maxWidth="xl" sx={{ paddingTop: 5, paddingBottom: 5 }}>
      <Typography
        variant="h3"
        sx={{
          fontWeight: 800,
          marginBottom: 4,
          color: "#111827",
        }}
      >
        Profile Manager
      </Typography>

      <LayoutCard title="Lista profili">
        <ProfileGrid profiles={profiles} onSelect={handleSelect} />
      </LayoutCard>

      <Grid container spacing={4} sx={{ marginTop: 1 }}>
          <LayoutCard title="Dodawanie profilu">
            <ProfileForm onSaved={loadProfiles} />
          </LayoutCard>

          <LayoutCard title="Szczegóły profilu">
            <ProfileDetails profile={selectedProfile} />
          </LayoutCard>
      </Grid>
    </Container>
  );
}

export default App;