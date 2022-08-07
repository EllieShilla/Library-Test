import { ReviewDTO } from "./ReviewDTO.model";

export interface BookDetailsDTO{
  Id:number;
  Title:string;
  Author:string;
  Cover:string;
  Content:string;
  Genre:string;
  Rating:number;
  Reviews:ReviewDTO[];
}
