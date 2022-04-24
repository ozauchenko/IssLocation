export interface Position {
    latitude: number;
    longitude: number;
}

export interface IssLocation {
    message: string;
    timestamp: number;
    position: Position;
}