import { Photo } from "./photo";

export interface Member {
  id: number
  username: string
  photoUrl: string
  age: number
  nickName: string
  created: Date
  lastActive: Date
  gender: string
  sport: string
  experience: number
  city: string
  country: string
  photos: Photo[]
}


