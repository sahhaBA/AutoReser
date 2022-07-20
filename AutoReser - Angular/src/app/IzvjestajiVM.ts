export class Izvjestaj {
  izvjestajID!: number;
  naziv!: string;
  opis!: string;
  automobilID!: string;
  uposlenikKreiraIzvjestaj!: string;
  datumIzvjestaja!: string;
  korisnickiNalogID!: string;
}

export class IzvjestajiVM{
  izvjestaji!: Izvjestaj[];
  q?: any;
  total!: number;
}

