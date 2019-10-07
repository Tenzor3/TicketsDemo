using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileHelpers;
using TicketsDemo.Data.Entities;
using TicketsDemo.Data.Repositories;

namespace TicketsDemo.CSV.Repositories
{
    public class CsvTrainRepository : ITrainRepository
    {
        // I have not idea where I should paste this configuration 
        private const string DataDirectory =
            "C:\\Users\\oleks\\Documents\\programming_university\\TicketsDemo\\TicketsDemoCSVData";

        private const string TrainFileName = "Train.csv";
        private const string CarriageFileName = "Carriage.csv";
        private const string PlaceFileName = "Place.csv";

        #region ITrainRepository Members

        public List<Train> GetAllTrains()
        {
            var engine = new FileHelperEngine<Train>();
            var result = engine.ReadFile(Path.Combine(DataDirectory, TrainFileName));

            foreach (var itemTrain in result)
            {
                itemTrain.Carriages = new List<Carriage>();
            }

            return result.ToList();
        }

        public Data.Entities.Train GetTrainDetails(int id)
        {
            var train = GetAllTrains().Single(t => t.Id == id);

            var carriageEngine = new FileHelperEngine<Carriage>();
            var placeEngine = new FileHelperEngine<Place>();

            var carriages = carriageEngine
                .ReadFile(Path.Combine(DataDirectory, CarriageFileName))
                .Where(t => t.TrainId == train.Id);

            train.Carriages = carriages.ToList();
            train.Carriages.ForEach(test => test.Train = train);

            var places = placeEngine
                .ReadFile(Path.Combine(DataDirectory, PlaceFileName))
                .Where(p => train.Carriages.Any(iterCarriage => iterCarriage.Id == p.CarriageId));

            train.Carriages.ForEach(carriage => carriage.Places = places.Where(place => place.CarriageId == carriage.Id).ToList());
            train.Carriages.ForEach(carriage => carriage.Places.ForEach(place => place.Carriage = carriage));

            return train;
        }

        public void CreateTrain(Data.Entities.Train train)
        {
            var trainEngine = new FileHelperEngine<Train>();
            var trains = new List<Train> {train};
            trainEngine.WriteFile(Path.Combine(DataDirectory, TrainFileName), trains);

            var carriagesEngine = new FileHelperEngine<Carriage>();
            carriagesEngine.WriteFile(Path.Combine(DataDirectory, CarriageFileName), train.Carriages);
            
            var placeEngine = new FileHelperEngine<Place>();
            foreach (var carriage in train.Carriages)
            {
                placeEngine.WriteFile(Path.Combine(DataDirectory, PlaceFileName), carriage.Places);
            }
        }

        public void UpdateTrain(Data.Entities.Train train)
        {
            var allTrains = GetAllTrains();
            var updatedTrainsList = new List<Train>();
            foreach (var trainItem in allTrains)
            {
                if (trainItem.Id == train.Id)
                {
                    updatedTrainsList.Add(train);
                    continue;
                }

                updatedTrainsList.Add(GetTrainDetails(trainItem.Id));
            }

            File.Delete(Path.Combine(DataDirectory, TrainFileName));
            File.Create(Path.Combine(DataDirectory, TrainFileName));

            foreach (var trainItem in updatedTrainsList)
            {
                CreateTrain(trainItem);
            }
        }

        public void DeleteTrain(Data.Entities.Train train)
        {
            var allTrains = GetAllTrains();
            var updatedTrainsList = (from trainItem in allTrains where trainItem.Id != train.Id select GetTrainDetails(trainItem.Id)).ToList();

            File.Delete(Path.Combine(DataDirectory, TrainFileName));
            File.Create(Path.Combine(DataDirectory, TrainFileName));

            foreach (var trainItem in updatedTrainsList)
            {
                CreateTrain(trainItem);
            }
        }

        #endregion
    }
}
