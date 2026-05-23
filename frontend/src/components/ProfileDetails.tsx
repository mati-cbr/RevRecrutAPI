import React from "react";
import { Profile } from "../models/profile";

interface Props {
  profile: Profile | null;
}

export const ProfileDetails: React.FC<Props> = ({ profile }) => {
  if (!profile) {
    return <div>Wybierz profil z listy</div>;
  }

  return (
    <div className="card">
      <h2>Szczegóły profilu</h2>

      <p><strong>ID:</strong> {profile.id}</p>
      <p><strong>Imię:</strong> {profile.firstName}</p>
      <p><strong>Nazwisko:</strong> {profile.lastName}</p>
      <p><strong>E-mail:</strong> {profile.contactEMail}</p>
      <p><strong>Telefon:</strong> {profile.contactPhone}</p>
      <p><strong>Adres 1:</strong> {profile.address1}</p>
      <p><strong>Adres 2:</strong> {profile.address2}</p>
    </div>
  );
};
