export class Methods {
    static formatDate(d: Date): Date {
        let dateString = d.toString();
        let dateFormatted = new Date(dateString + 'Z');
        
        return dateFormatted;
    }
}