﻿namespace Xioru.Grain.Contracts.GrainReadModel
{
    public class GrainDescription
    {
        public string GrainName { get; set; } = default!;

        public string GrainType { get; set; } = default!;

        public Guid GrainId { get; set; } = default!;
    }
}
