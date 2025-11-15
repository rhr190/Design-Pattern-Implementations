
// Componenet interface
interface DataSource {
    writeData(data: string): void;
    readData(): string;
}

// Concrete Component
class FileDataSource implements DataSource {
    private filePath: string;
    private data: string;
    constructor(filePath: string) {
        this.filePath = filePath;
    }

    writeData(data: string): void {
        console.log(`Writing data to file at ${this.filePath}: ${data} using ${FileDataSource.name}`);
        this.data = data;
    }

    readData(): string {
        console.log(`Reading data from file at ${this.filePath} using ${FileDataSource.name}`);
        return this.data;
    }
}


// base decorator
class DataSourceDecorator implements DataSource {
    protected wrapee: DataSource;

    constructor(source: DataSource) {
        this.wrapee = source;
    }

    writeData(data: string): void {
        console.log(`DataSourceDecorator: Delegating writeData to wrapee`);
        this.wrapee.writeData(data);
    }

    readData(): string {
        console.log(`DataSourceDecorator: Delegating readData to wrapee`);
        return this.wrapee.readData();
    }
}


// concrete decorator

class EncryptionDecorator extends DataSourceDecorator {

    constructor(source: DataSource) {
        super(source);
    }

    writeData(data: string): void {
        console.log(`EncryptionDecorator: Encrypting data before writing`);
        // Simple mock encryption (reversing the string)
        const encryptedData = data.split('').reverse().join('');
        super.writeData(encryptedData);
    }

    readData(): string {
        const data = super.readData();
        console.log(`EncryptionDecorator: Decrypting data after reading`);
        // Simple mock decryption (reversing the string back)
        return data.split('').reverse().join('');
    } 
}


class CompressionDecorator extends DataSourceDecorator {

    constructor(source: DataSource) {
        super(source);
    }

    writeData(data: string): void {
        console.log(`CompressionDecorator: Compressing data before writing`);
        // Simple mock compression (replacing whitespaces with semicolon)
        const compressedData = data.replace(/\s+/g, ';');
        super.writeData(compressedData);
    }

    readData(): string {
        const data = super.readData();
        console.log(`CompressionDecorator: Decompressing data after reading`);
        // Simple mock decompression (replacing semicolon back to whitespaces)
        return data.replace(/;/g, ' ');
    }
}

// Usage example
const fileDataSource = new FileDataSource('data.txt');
const encryptedDataSource = new EncryptionDecorator(fileDataSource);
const compressedAndEncryptedDataSource = new CompressionDecorator(encryptedDataSource);
compressedAndEncryptedDataSource.writeData('Hello World');
const result = compressedAndEncryptedDataSource.readData();
console.log(`Final result: ${result}`);