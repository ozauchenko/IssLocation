export const timestampToDate = (timestamp: number) =>
    new Date(timestamp * 1000).toISOString().slice(0, 19).replace('T', ' ');