import { Profile } from "../models/profile";

const API_URL = "https://localhost:7201/api/Profile";

export async function getProfiles(): Promise<Profile[]> {
  const response = await fetch(API_URL);

  if (!response.ok) {
    throw new Error("Błąd pobierania profili");
  }

  return response.json();
}

export async function getProfileById(id: number): Promise<Profile> {
  const response = await fetch(`${API_URL}/${id}`);

  if (!response.ok) {
    throw new Error("Błąd pobierania profilu");
  }

  const data = await response.json();

  return Array.isArray(data) ? data[0] : data;
}

export async function createProfile(profile: Profile): Promise<void> {
  const response = await fetch(API_URL, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(profile),
  });

  if (!response.ok) {
    throw new Error("Błąd zapisu profilu");
  }
}
